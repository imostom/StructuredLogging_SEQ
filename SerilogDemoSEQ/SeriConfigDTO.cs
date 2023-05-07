namespace SerilogDemoSEQ
{
    public class SeriConfigDTO
    {
        public object[] Using { get; set; }
        public Minimumlevel MinimumLevel { get; set; }
        public string[] Enrich { get; set; }
        public Writeto[] WriteTo { get; set; }
    }

    public class Minimumlevel
    {
        public string Default { get; set; }
        public Override Override { get; set; }
    }

    public class Override
    {
        public string Microsoft { get; set; }
        public string System { get; set; }
    }

    public class Writeto
    {
        public string Name { get; set; }
        public Args Args { get; set; }
        public string name { get; set; }
    }

    public class Args
    {
        public string path { get; set; }
        public string outputTemlate { get; set; }
        public string formatter { get; set; }
        public string serverUrl { get; set; }
    }

}
