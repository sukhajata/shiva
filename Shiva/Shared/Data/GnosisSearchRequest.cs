using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Data
{
    public class GnosisSearchRequest
    {
        private int entityID;
        private int systemID;
        private List<GnosisSearchParameter> searchParams;
        private string searchAction;
        private string autoSearchAction;

        public int EntityID {  get { return entityID; } }
        public int SystemID { get { return systemID; } }
        public List<GnosisSearchParameter> SearchParams { get { return searchParams; } }
        public string SearchAction { get { return searchAction; } }
        public string AutoSearchAction { get { return autoSearchAction; } }

        public GnosisSearchRequest(int _entityID, int _systemID, List<GnosisSearchParameter> _searchParams,
            string _searchAction, string _autoSearchAction)
        {
            entityID = _entityID;
            systemID = _systemID;
            searchParams = _searchParams;
            searchAction = _searchAction;
            autoSearchAction = _autoSearchAction;
        }
    }
}
