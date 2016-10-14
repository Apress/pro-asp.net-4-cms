using System;
using System.Configuration;
using IronPython.Hosting;
using IronRuby;
using IronRuby.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using CommonLibrary.DynamicLanguages;

namespace Business.Scripting
{
    public class DLRManager
    {
        private ScriptEngine _engine;
        private readonly ScriptScope _scope;

        /// <summary>
        /// Creates an instance of the ScriptEngine and ScriptScope automatically
        /// </summary>
        public DLRManager(Languages language)
        {
            _engine = CreateEngine(language);
            _scope = _engine.CreateScope();
        }

        /// <summary>
        /// Executes a dynamic script from the web.config-dictated location.
        /// </summary>
        /// <param name="fileName">The name of the script file.</param>
        /// <param name="className">The class to instantiate.</param>
        /// <param name="methodName">The method to execute.</param>
        /// <param name="parameters">Any parameters the method needs to execute successfully.</param>
        /// <returns>Dynamic; dictated by the returned information (if any) of the script.</returns>
        public dynamic ExecuteFile(string fileName, string className, string methodName,
                                   [ParamDictionary] params dynamic[] parameters)
        {
            try
            {
                _engine.ExecuteFile(ConfigurationManager.AppSettings["ScriptsFolder"] + @"\" + fileName, _scope);
                dynamic classObj = _scope.GetVariable(className);
                dynamic classInstance = _engine.Operations.Call(classObj);
                dynamic classMethod = _engine.Operations.GetMember(classInstance, methodName);
                dynamic results = parameters != null ? _engine.Operations.Call(classMethod, parameters) : _engine.Operations.Call(classMethod);
                return results;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Executes an arbitrary-length dynamic script.
        /// </summary>
        /// <param name="script">The dynamic code to execute.</param>
        /// <returns>Dynamic; dictated by the returned information (if any) of the script.</returns>
        public dynamic Execute(string script)
        {
            try
            {
                return _engine.Execute(script, _scope);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Creates an engine of the relevant DLR language type.
        /// </summary>
        /// <param name="language">the strongly-typed language name</param>
        private ScriptEngine CreateEngine(Languages language)
        {
            switch (language)
            {
                case Languages.IronPython:
                    return Python.CreateEngine();

                case Languages.IronRuby:
                    return Ruby.CreateEngine();            
       
                default:
                    return null;
            }
        }
    }
}