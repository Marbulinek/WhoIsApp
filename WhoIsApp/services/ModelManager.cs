using F23.StringSimilarity;
using WhoIsApp.models;

namespace WhoIsApp.services
{
    public class ModelManager
    {
        public List<PropertyItem> Model { get; set; }

        public ModelManager()
        {
            Model = new List<PropertyItem>();
        }

        public void PrepareModel(string whoisResponseText)
        {
            whoisResponseText = NormalizationService.NormalizeStars(whoisResponseText);

            whoisResponseText = NormalizationService.NormalizeNewLineDividers(whoisResponseText);

            var splittedResults = whoisResponseText.Split("\n");
            
            var preservedKey = string.Empty;
            for (int i = 0; i < splittedResults.Length; i++)
            {

                var line = splittedResults[i];
                var actualLine = line.Split(":", 2);
                if (actualLine.Length == 2)
                {
                    // classic key:value
                    var propertyItem = new PropertyItem() { PositionFound = Model.Count + 1, Key = actualLine[0].ToLowerInvariant().Trim(), Value = actualLine[1].Trim() };
                    preservedKey = propertyItem.Key;
                    // only if value is filled
                    if(propertyItem.Value != "-")
                    {
                        Model.Add(propertyItem);
                    }
                }else if(actualLine.Length == 1)
                {
                    //assign previous key

                    //check previous line if it was empty
                    if ((i-1 >= 0) && splittedResults[i - 1] != null)
                    {
                        var previousLine = splittedResults[i - 1];
                        if (string.IsNullOrEmpty(previousLine))
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(preservedKey) && !string.IsNullOrEmpty(actualLine[0].Trim()))
                    {
                        var propertyItem = new PropertyItem() { PositionFound = Model.Count + 1, Key = preservedKey, Value = actualLine[0].Trim() };
                        if (propertyItem.Value != "-")
                        {
                            Model.Add(propertyItem);
                        }
                    }
                }                
            }
        }

        public string SearchModel(string[] searchKeys, int usePosition = -1)
        {
            var probabilityItemList = new List<ProbabilityItem>();

            foreach (string key in searchKeys)
            {
                IEnumerable<PropertyItem> items = Model.Where(x => x.Key.ToLowerInvariant().Contains(key.ToLowerInvariant())).OrderBy(x => x.PositionFound);
                foreach(var item in items)
                {
                    var similiarity = this.ComputeSimiliarity(item.Key.ToLowerInvariant(), key.ToLowerInvariant());
                    probabilityItemList.Add(new ProbabilityItem() { Key = item.Key, Value = item.Value, PositionFound = item.PositionFound, Similiarity = similiarity });
                }
            }

            probabilityItemList = probabilityItemList.OrderByDescending(x => x.Similiarity).ThenBy(x => x.PositionFound).ToList();
            var result = probabilityItemList.FirstOrDefault();
            if(result != null)
            {
                Model.RemoveAll(x => x.PositionFound == result.PositionFound);
                return result.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        private double ComputeSimiliarity(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return 0;
            }

            var jw = new JaroWinkler();

            double distance = jw.Similarity(s1, s2);
            return distance;
        }
    }
}
