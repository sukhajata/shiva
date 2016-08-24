using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Shiva.Shared.BaseControllers;


using Shiva.Shared.Data;
using Shiva.Shared.GenericControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using GnosisControls;
//using System.Data;

namespace Shiva.Shared.Events
{
    public class GnosisBooleanHelper
    {

        private static Regex regexCondition = new Regex(@"(Not )?\{([a-zA-Z]+)\}");
        private static Regex regexGnosisCondition = new Regex(@"(Not )?<[0-9]+>");
        private static Regex regexBrackets = new Regex(@"\([^()]+\)");
        private static Regex regexPropertyName = new Regex(@"(?<=\{)[a-zA-Z]+(?=\})");
        private static Regex regexBooleans = new Regex(@"(True)|(False)");
		private static Regex regexAnd = new Regex (@"[(True)|(False)][\s]+(and)[\s]+[(True)|(False)]");

        private GnosisEventHandler handler;
        private GnosisControl ownerControl;

        public GnosisBooleanHelper(GnosisEventHandler _handler, GnosisControl _control)
        {
            handler = _handler;
            ownerControl = _control;
        }

        public bool Evaluate(string input)
        {
            bool result = false;

            if (input.Contains("("))
            {
                Match match = regexBrackets.Match(input);
                bool res = EvaluateSequence(match.ToString());
                input = input.Replace(match.ToString(), res.ToString());
                result = Evaluate(input);
            }
            else
            {
                result = EvaluateSequence(input);
            }

            return result;
        }


        private  bool EvaluateSequence(string input)
        {
            MatchCollection conditions = regexCondition.Matches(input);
            foreach (Match condition in conditions)
            {
                //Find the source control and evaluate the relevant property
                //We do not have the id of the source control. We find it by type.
                //It may reference the control on which the event was raised - ownerControl
                string propertyName = regexPropertyName.Match(condition.ToString()).ToString();
                GnosisEventHandlerSource source = FindEventHandlerSource(propertyName);
                if (source == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Could not find event handler source for " + propertyName, "GnosisBooleanHelper.EvaluateSequence");
                }

                bool result = false;
                string sourceControlType = source.ControlType;
                if (ownerControl.ControlType.Equals(sourceControlType))
                {
                    //check property of ownerControl
                    PropertyInfo prop = ownerControl.GetType().GetProperty(propertyName);
                    result = (bool)prop.GetValue(ownerControl, null);
                }
                else if (sourceControlType.Equals("Connection"))
                {
                    GnosisConnection connection = GlobalData.Singleton.Connection;
                    PropertyInfo propInfo = connection.GetType().GetProperty(propertyName);
                    result = (bool)propInfo.GetValue(connection, null);
                }
                else if (sourceControlType.Equals("Generic Menu Item"))
                {
                    GnosisGenericMenuItemController target = GnosisGenericMenuItemController.CurrentMenuItemController;
                    PropertyInfo propInfo = target.GetType().GetProperty(propertyName);
                    result = (bool)propInfo.GetValue(target, null);
                }
                else if (sourceControlType.Equals("Navigator Tile"))
                {
                    GnosisNavTileController target = GlobalData.Singleton.PrimarySplitController.NavTileController;
                    PropertyInfo propInfo = target.GetType().GetProperty(propertyName);
                    result = (bool)propInfo.GetValue(target, null);
                }
                else
                {
                    //Look for the GenericControlController
                    GnosisGenericControlController genericControlController = GlobalData.Singleton.FindGenericControllerByType(sourceControlType);
                    if (genericControlController != null)
                    {
                        GnosisVisibleController target = genericControlController.CurrentInstance;
                        PropertyInfo propInfo = target.GetType().GetProperty(propertyName);
                        result = (bool)propInfo.GetValue(target, null);
                    }
                    else
                    {
                        GlobalData.Singleton.ErrorHandler.HandleError("GenericControl not found of type " + sourceControlType.ToString(),
                            "GnosisBooleanHelper.EvaluateSequence");
                    }
                }

                if (condition.ToString().Contains("Not"))
                {
                    result = !result;
                }
                input = input.Replace(condition.ToString(), result.ToString());
            }
				

            bool sequenceResult = Eval(input);

            //MatchCollection booleans = regexBooleans.Matches(input);
            //List<bool> results = new List<bool>();
            //foreach (Match m in booleans)
            //{
            //    results.Add(Boolean.Parse(m.ToString()));
            //}
            //if (input.Contains("and"))
            //{
            //    if (results.Any(x => x == false))
            //    {
            //        sequenceResult = false;
            //    }
            //    else
            //    {
            //        sequenceResult = true;
            //    }

            //}
            //else if (input.Contains("or"))
            //{
            //    if (results.Any(x => x == true))
            //    {
            //        sequenceResult = true;
            //    }
            //    else
            //    {
            //        sequenceResult = false;
            //    }
            //}
            //else //only one condition
            //{
            //    if (input.Contains("True"))
            //    {
            //        sequenceResult = true;
            //    }
            //    else
            //    {
            //        sequenceResult = false;
            //    }
            //}

            return sequenceResult;

        }

        private bool Eval(String expression)
        {
           // System.Data.DataTable table = new System.Data.DataTable();
           // bool result = Convert.ToBoolean(table.Compute(expression, String.Empty));
			bool result = false;
			MatchCollection booleans;
			List<bool> results;

			expression = expression.Replace(")", "").Replace("(", "").Trim();

			//evaluate 'and's first
			MatchCollection ands = regexAnd.Matches (expression);

			foreach (Match m in ands) {
				bool andResult = true;

				booleans = regexBooleans.Matches(m.ToString());
				results = new List<bool>();

				foreach (Match mBool in booleans) {
				   results.Add(Boolean.Parse(mBool.ToString()));
				}

				if (results.Any(x => x == false))  {
					andResult = false;
				}

				expression = expression.Replace (m.ToString (), andResult.ToString ());
			}

			booleans = regexBooleans.Matches(expression);
			results = new List<bool>();

			foreach (Match mBool in booleans) {
				results.Add(Boolean.Parse(mBool.ToString()));
			}

			if (results.Any(x => x == true))  {
				result = true;
			}

			EventLogger.LogSourceExpression(expression, result);

            return result;
        }

        private GnosisEventHandlerSource FindEventHandlerSource(string propertyName)
        {
            foreach (GnosisEventHandlerSource source in handler.GnosisEventHandlerSources)
            {
                if (source.SourceProperty.Equals(propertyName))
                {
                    return source;
                }
            }

            return null;
        }
    }
    }
