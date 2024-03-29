﻿using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    public class HistoricalConversionModel {
        public static readonly object x = new object();

        public int Id { get; set; }

        public string Year { get; set; }

        public int Order { get; set; }

        public decimal Modifier { get; set; }

        public string ModifierFormated {
            get {
                return string.Format("x {0}", Modifier);
            }
        }

        public static HistoricalConversionModel MapFrom(HistoricalConversion entity) {
            var model = new HistoricalConversionModel();

            if (entity != null) {
                model.Id = entity.Id;
                model.Year = entity.Year;
                model.Order = entity.Order;
                model.Modifier = entity.Modifier;
            }

            return model;
        }
    }
}