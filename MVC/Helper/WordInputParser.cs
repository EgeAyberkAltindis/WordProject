
using MVC.Models;
using MVC.Models.RequestModel;

using Newtonsoft.Json.Linq;

namespace MVC.Helper
{
    public static class WordInputParser
    {
        public static List<AddFullWordInputModel> ParseMultiple(string rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
                throw new ArgumentException("Giriş boş olamaz.");

            var lines = rawInput
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var result = new List<AddFullWordInputModel>();

            for (int i = 0; i < lines.Count - 1; i += 2)
            {
                var header = lines[i].Split(':', 2);
                var english = header[0].Trim();

                var turkishList = header.Length > 1
                    ? header[1].Split(new[] { ',', '/' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => x.Trim())
                               .ToList()
                    : new List<string>();

                var sentenceLine = lines[i + 1];
                var sentences = sentenceLine.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => x.Trim())
                                            .ToList();

                result.Add(new AddFullWordInputModel
                {
                    English = english,
                    TurkishList = turkishList,
                    Sentences = sentences
                });
            }

            return result;
        }
        public static AddFullWordInputModel Parse(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new ArgumentException("Giriş metni boş olamaz.");

            var lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries);

           
            var header = lines[0].Split(':');
            var english = header[0].Trim();
            var turkishList = header.Length > 1
                ? header[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(x => x.Trim())
                           .ToList()
                : new List<string>();

           
            var sentenceLine = lines.Length > 1 ? lines[1] : "";
            var sentences = sentenceLine.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(x => x.Trim())
                                        .ToList();

            return new AddFullWordInputModel
            {
                English = english,
                TurkishList = turkishList,
                Sentences = sentences
            };
        }
    }
}
