using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;

namespace ExpressBase.Studio.pDesigner
{
    public class TypeDiscoveryService : ITypeDiscoveryService
    {
        readonly static List<Assembly> designerAssemblies = new List<Assembly>();

        /// <summary>
        /// List of assemblies used by the form designer. This static list is not an optimal solution,
        /// but better than using AppDomain.CurrentDomain.GetAssemblies(). See SD2-630.
        /// </summary>
        public static List<Assembly> DesignerAssemblies
        {
            get
            {
                return designerAssemblies;
            }
        }

        public TypeDiscoveryService()
        {
            //DesignerAssemblies.Add(typeof(object).Assembly);
            //DesignerAssemblies.Add(typeof(Uri).Assembly);
            //DesignerAssemblies.Add(typeof(System.ComponentModel.Component).Assembly);
            //DesignerAssemblies.Add(typeof(System.Drawing.Point).Assembly);
            //DesignerAssemblies.Add(typeof(System.Windows.Forms.Design.AnchorEditor).Assembly);
            DesignerAssemblies.Add(typeof(System.Windows.Forms.Control).Assembly);

            DesignerAssemblies.Add(typeof(ExpressBase.Studio.Controls.IEbControl).Assembly);
            DesignerAssemblies.Add(typeof(ExpressBase.Studio.Controls.IEbControlContainer).Assembly);

            DesignerAssemblies.Add(typeof(ExpressBase.Objects.EbControl).Assembly);
        }

        /// <summary>
        /// Returns the list of available types.
        /// </summary>
        /// <param name="baseType">The base type to match.  Can be null.</param>
        /// <param name="excludeGlobalTypes">Determines whether types
        /// from all referenced assemblies should be checked.</param>
        public ICollection GetTypes(Type baseType, bool excludeGlobalTypes)
        {
            List<Type> types = new List<Type>();

            if (baseType == null)
            {
                baseType = typeof(object);
            }

            //seek in all assemblies
            //allow to work designers like columns editor in datagridview
            // Searching types can cause additional assemblies to be loaded, so we need to use
            // ToArray to prevent an exception if the collection changes.
            foreach (Assembly asm in DesignerAssemblies.ToArray())
            {
                if (excludeGlobalTypes)
                    continue;

                try
                {
                    AddDerivedTypes(baseType, asm, types);
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (BadImageFormatException)
                {
                    // ignore assembly load errors
                }
            }

            // TODO - Don't look in all assemblies.
            // Should use the current project and its referenced assemblies
            // as well as System.Windows.Forms.

            return types;
        }

        /// <summary>
        /// Gets the types derived from baseType from the assembly and adds them to the list.
        /// </summary>
        void AddDerivedTypes(Type baseType, Assembly assembly, IList<Type> list)
        {
            foreach (Type t in assembly.GetExportedTypes())
            {
                if (t.IsSubclassOf(baseType))
                    list.Add(t);
            }
        }
    }
}

