using CommonLibrary.Interfaces;
using CommonLibrary.DynamicLanguages;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of a dynamic language script
    /// </summary>
    public class ScriptedFile : IScriptedFile
    {
        public ScriptedFile(dynamic[] parameters)
        {
            this.parameters = parameters;
        }

        public string fileName { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public dynamic[] parameters { get; set; }
        public Languages language { get; set; }
    }
}