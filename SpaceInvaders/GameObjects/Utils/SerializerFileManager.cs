using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Utils
{
    /// <summary>
    /// Class for the serializer file manager
    /// </summary>
    internal class SerializerFileManager
    {

        #region methods
        /// <summary>
        ///  Method to serialize a dictionary
        /// </summary>
        /// <typeparam name="Object"> Type of the dictionary</typeparam>
        /// <param name="dictionary"> Dictionary to serialize</param>
        /// <param name="stream"> Stream to write the dictionary</param>
        public static void Serialize<Object>(Object dictionary, Stream stream)
        {
            try // try to serialize the collection to a file
            {
                using (stream)
                {
                    // create BinaryFormatter
                    BinaryFormatter bin = new BinaryFormatter();
                    // serialize the collection (EmployeeList1) to file (stream)
                    bin.Serialize(stream, dictionary);
                }
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        ///  Method to deserialize a dictionary
        /// </summary>
        /// <typeparam name="Object"> Type of the dictionary</typeparam>
        /// <param name="stream"> Stream to read the dictionary</param>
        /// <returns></returns>
        public static Object Deserialize<Object>(Stream stream) where Object : new()
        {
            Object ret = CreateInstance<Object>();
            try
            {
                using (stream)
                {
                    // create BinaryFormatter
                    BinaryFormatter bin = new BinaryFormatter();
                    // deserialize the collection (Employee) from file (stream)
                    ret = (Object)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
            return ret;
        }

        /// <summary>
        ///  Method to create an instance of a generic type
        /// </summary>
        /// <typeparam name="Object"> Type of the instance</typeparam>
        /// <returns></returns>
        public static Object CreateInstance<Object>() where Object : new()
        {
            return (Object)Activator.CreateInstance(typeof(Object));
        }
        #endregion
    }
}
