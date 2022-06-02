using System;

namespace SmokeLogic
{
    public class Property
    {
        public int Id { get; set; }
        public int? gameId { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
}
