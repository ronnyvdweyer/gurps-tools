using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCv20.Tools.Core.Domain;
using SCv20.Tools.Core.DataContext;

namespace SCv20.Tools.Core.Services {
    public class AssetService {
        private static AssetService _instance;

        private AssetService() {
            
        }

        public static AssetService GetInstance() {
            lock (typeof(AssetService)) {
                if (_instance == null)
                    _instance = new AssetService();

                return _instance;
            }
        }

        public void Create(Asset asset, object assetData) {
            var db = RulesContext.GetInstance();
            asset.Data = SerializationService.GetInstance().Serialize(assetData);
            db.Assets.Add(asset);
            db.SaveChanges();
        }
    }
}
