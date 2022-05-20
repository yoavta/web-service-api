using System.ComponentModel.DataAnnotations;

namespace web_service_api
{
    public class Ranking
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public int Rank { get; set; }
    }
}
