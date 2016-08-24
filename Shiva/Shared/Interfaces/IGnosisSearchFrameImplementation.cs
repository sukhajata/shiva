using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisSearchFrameImplementation : IGnosisFrameImplementation
    {
        [GnosisCollection]
        List<GnosisSearchParameter> SearchParameters { get; set; }

        [GnosisChild]
        GnosisSearchResultsGrid SearchResultsGrid { get; set; }
    }
}
