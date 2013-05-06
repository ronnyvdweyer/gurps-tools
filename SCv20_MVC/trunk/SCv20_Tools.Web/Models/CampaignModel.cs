using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    [HandleError]
    public class CampaignModel {
        public static readonly object x = new object();

        public CampaignModel() {
            ListHistoricalConversions = new List<HistoricalConversionModel>();
            ListCampaignQualities = new List<QualityModel>();
            ListAvaliableQualities = new List<QualityModel>();
        }

        public int Id { get; set; }

        public int BaseReputation { get; set; }

        public decimal BaseNetWorth { get; set; }

        public int StartingLevel { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string YearDetails { get; set; }

        public string Concept { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }

        #region -- Calculated Fields -----------------------------------------------------

        public string BaseNetWorthFormatted {
            get {
                return String.Format("{0:C0}", BaseNetWorth);
            }
        }

        public int BaseXPCalculated {
            get {
                return ListCampaignQualities.Sum(item => item.BonusXP);
            }
        }

        public int BaseADCalculated {
            get {
                return ListCampaignQualities.Sum(item => item.BonusAD);
            }
        }

        public string BaseXPFormatted {
            get {
                return BaseXPCalculated.ToString("+#;-#;+0") + " XP";
            }
        }

        public string BaseADFormatted {
            get {
                return BaseADCalculated.ToString("+#;-#;+0") + " AD";
            }
        }

        #endregion -- Calculated Fields -----------------------------------------------------

        #region -- View Supporting Fields ------------------------------------------------

        public int SelectedQualityId { get; set; }

        public int SelectedYearId { get; set; }

        public IList<QualityModel> ListCampaignQualities { get; set; }

        public List<QualityModel> ListAvaliableQualities { get; set; }

        public IList<HistoricalConversionModel> ListHistoricalConversions { get; set; }

        #endregion -- View Supporting Fields ------------------------------------------------

        public static CampaignModel MapFrom(Campaign entity) {
            var model = new CampaignModel();

            if (entity != null) {
                model.Id = entity.Id;
                model.BaseReputation = entity.BaseReputation;
                model.BaseNetWorth = entity.BaseNetWorth;
                model.StartingLevel = entity.StartingLevel;
                model.Code = entity.Code;
                model.Name = entity.Name;
                model.YearDetails = entity.YearDetails;
                model.Concept = entity.Concept;
                model.Summary = entity.Summary;
                model.CreatedOn = entity.CreatedOn;
                model.SelectedYearId = entity.YearId;
                model.ListCampaignQualities = entity.Qualities.Select(row => QualityModel.MapFrom(row.Quality)).OrderBy(row => row.Name).ToList();
            }
            return model;
        }
    }
}