using System;
using CommonLibrary.DynamicLanguages;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Defines the properties of a dynamic script file
    /// </summary>
    public interface IScriptedFile
    {
        string className { get; set; }
        string fileName { get; set; }
        string methodName { get; set; }
        dynamic[] parameters { get; set; }
        Languages language { get; set; }
    }
}
