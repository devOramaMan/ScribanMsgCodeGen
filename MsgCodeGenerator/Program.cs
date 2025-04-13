

using System;
using System.IO;
using System.Text.Json;
using Scriban;
using CommandLine;
using Microsoft.Extensions.Logging;
using Scriban.Parsing;




class Program
{
    static private ILogger? logger;
    public class Options
    {
        [Option('o', "out", Required = false, HelpText = "Output folder", Default = "../../../../MsgBinaryConverter/AppMsg")]
        public string? MsgOutFolder { get; set; }

        [Option('u', "unit", Required = false, HelpText = "Output folder Unit test", Default = "../../../../MsgBinaryConverterUnitTest")]
        public string? UnitOutFolder { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }


    public static string Render(string templateStr, object? obj = null)
    {
        var template = Template.Parse(templateStr);
        if (template.HasErrors)
            throw new Exception(string.Join("\n", template.Messages.Select(x => $"{x.Message} at {x.Span.ToStringSimple()}")));
        return template.Render(obj, member => LowerFirstCharacter(member.Name));
    }

    private static string LowerFirstCharacter(string value)
    {
        if (value.Length > 1)
            return char.ToLower(value[0]) + value.Substring(1);
        return value;
    }

    private static string TemplateAreasMsgClass(object model, ILogger? logger)
    {

        // Load Scriban template
        var templateFilePath = "areas.scriban";
        var templateContent = File.ReadAllText(templateFilePath);


        // Render the template with JSON data
        string result = Render(templateContent, new { model });
        logger?.LogDebug(result);
        return result;
    }

    private static string templateCommonMsgClass(object model)
    {
        // Load Scriban template
        var templateFilePath = "common.scriban";
        var templateContent = File.ReadAllText(templateFilePath);

        // Render the template with JSON data
        string result = Render(templateContent, new { model });

        logger?.LogDebug(result);
        return result;
    }

    private static void scribanonline_azurewebsites_net()
    {
        string jsonString = @"{ ""name"" : ""Bob Smith"", ""address"" : ""1 Smith St, Smithville"", ""orderId"" : ""123455"", ""total"" : 23435.34, ""items"" : [ { ""name"" : ""1kg carrots"", ""quantity"" : 1, ""total"" : 4.99 }, { ""name"" : ""2L Milk"", ""quantity"" : 1, ""total"" : 3.5 } ] }";

        // Deserialize the JSON string into a dynamic object
        //var model = JsonConvert.DeserializeObject<dynamic>(jsonString);
        var model = JsonSerializer.Deserialize<JsonElement>(jsonString);

        // Define the Scriban template
        string templateText = @"
Dear {{ model.name }},

Your order, {{ model.orderId}}, is now ready to be collected.

Your order shall be delivered to {{ model.address }}.  If you need it delivered to another location, please contact as ASAP.

Order: {{ model.orderId}}
Total: {{ model.total | math.format ""c"" ""en-US"" }}

Items:
------
{{- for item in model.items }}
 * {{ item.quantity }} x {{ item.name }} - {{ item.total | math.format ""c"" ""en-US"" }}
{{- end }}

Thanks,
BuyFromUs
        ";

        // Parse the template
        //var template = ParseTemplate(templateText);

        // Render the template with the model
        string result = Render(templateText, new { model });

        // Output the result
        Console.WriteLine(result);
    }

    static void writeFiles(string output, Options opts)
    {
        var classlist = output.Split(":Separator##");

        //delete last


        for (int i = 0; i < classlist.Length - 1; i++)
        {
            string class_str = classlist[i];
            // Get the last segment from ##
            int idx = class_str.LastIndexOf("##");

            if (idx == -1)
                throw new Exception("Missing filename sepparator");

            
            string filename = $"{class_str.Substring(idx+2)}.cs";
            if (filename.Contains("Test"))
                filename = $"{opts.UnitOutFolder}/{filename}";
            else
                filename = $"{opts.MsgOutFolder}/{filename}";
                var content = class_str.Substring(0, idx);

            //Write to file
            File.WriteAllText(filename, content);

        }
    }

    static void Main(string[] args)
    {       

        var opts = CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => RunOptions(opts))
            .WithNotParsed<Options>((errs) => HandleParseError(errs));
        

        //scribanonline_azurewebsites_net();
    }
    static void RunOptions(Options opts)
    {
        LogLevel loglevel = opts.Verbose ? LogLevel.Debug : LogLevel.Information;
        
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddSimpleConsole(option => 
                {
                    option.SingleLine = true;
                    option.TimestampFormat = "yyyy-MM-dd HH:mm:ss";
                })
                .SetMinimumLevel(loglevel);
        });

        logger = loggerFactory.CreateLogger<Program>();

        if (opts.MsgOutFolder == null || opts.UnitOutFolder == null) {
            throw new Exception("Missing ouput folder");
        }

        logger?.LogDebug($"Source: {opts.MsgOutFolder}");
        logger?.LogDebug($"Unit: {opts.UnitOutFolder}");

        // Load JSON data
        var jsonFilePath = "MsgContainer.json";
        var jsonContent = File.ReadAllText(jsonFilePath);
        var model = JsonSerializer.Deserialize<JsonElement>(jsonContent);
        string result = "";

        result = templateCommonMsgClass(model);

        result += TemplateAreasMsgClass(model,
        logger);

        writeFiles(result, opts);
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        Console.WriteLine("Error parsing arguments.");
    }
}
