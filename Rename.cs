using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FileSort
{
    public static class R
    {
        public static bool Rename(string source, string dest)
        {
            try
            {
                File.Move(source, dest);
            }
            catch (IOException e)
            {
                Trace.WriteLine("Level 1 Rename failed: " + e);
            }
            return false;
        }

        /// <summary>
        /// Level 2 method for renaming an array of files.
        /// the two arrays must match up in length or else a IOException will be thrown 
        /// </summary>
        /// <param name="sources">the array of files to rename, these MUST be absolute paths</param>
        /// <param name="dests">the array of new file names, these MUST be absolute paths</param>
        /// <returns>whether the process completed sucessfully</returns>
        public static bool Rename(string[] sources, string[] dests)
        {
            if (sources == null || dests == null || sources.Length == 0 || sources.Length != dests.Length)
                throw new ArgumentException("Level 2 Rename: Null arrays or 0 length or mismatched lengths");

            bool result = true;
            try
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    result &= Rename(sources[i], dests[i]);
                }
                return result;
            }
            catch (IOException e)
            {
                Trace.WriteLine("Level 3 Rename failed: " + e);
            }
            return false;
        }

        /// <summary>
        /// top most level rename method
        /// will rename all the files within the directory according to the title and index
        /// 
        /// will have the format: 
        /// (Title)(space)(padding)(index).(original file extension) 
        /// </summary>
        /// <param name="directory">the absolute path of the file</param>
        /// <param name="title">the title to be used in front</param>
        /// <param name="index">The starting index to rename files</param>
        /// <param name="padding">Whether to pad the indices with zeroes</param>
        /// <param name="space">Whether to insert a space between the title and index</param>
        /// <returns></returns>
        public static bool Rename(string directory, string title = "", int index = 1, bool space = true, bool padding = false)
        {
            //get the files in the directory. then feed them into the level 4 rename method. 

            string[] sources = Directory.GetFiles(directory); 
	    /*
            string[] dests = new string[sources.Length];

            #region formatting
            string format;
            //formatting the final names
            int pad = 0;
            string pa = ""; //the number of zeroes to put 
            if (padding)
            {
                pad = (int)Math.Log10(sources.Length) + 1;
                for (int i = 0; i < pad; i++)
                {
                    pa += "0";
                }

            }
            //for spaces
            string sp = "";
            if (space)
                sp = " ";


            format = "{0}\\{1}" + sp + "{2:" + pa + "}{3}";
            #endregion

            for (int i = 0; i < sources.Length; i++)
            {
                dests[i] = string.Format(format, sources[i].Substring(0, sources[i].LastIndexOf('\\')), title, i + index, sources[i].Substring(sources[i].LastIndexOf('.')));
            }
	    */
            return Rename(sources, GenerateDestinations(sources,title,index,space,padding));
        }

	/// <summary>
	/// See Rename Level 4 usage. 
	/// </summary>
	/// <param name="sources"></param>
	/// <param name="title"></param>
	/// <param name="index"></param>
	/// <param name="space"></param>
	/// <param name="padding"></param>
	/// <returns></returns>
        public static string[] GenerateDestinations(string[] sources, string title = "", int index = 1, bool space = true, bool padding = false)
        {
            string[] dests = new string[sources.Length];

            #region formatting
            string format;
            //formatting the final names
            int pad = 0;
            string pa = ""; //the number of zeroes to put 
            if (padding)
            {
                pad = (int)Math.Log10(sources.Length) + 1;
                for (int i = 0; i < pad; i++)
                {
                    pa += "0";
                }

            }
            //for spaces
            string sp = "";
            if (space && !string.IsNullOrWhiteSpace(title))
                sp = " ";


            format = "{0}\\{1}" + sp + "{2:" + pa + "}{3}";
            #endregion

            for (int i = 0; i < sources.Length; i++)
            {
                dests[i] = string.Format(format, sources[i].Substring(0, sources[i].LastIndexOf('\\')), title, i + index, sources[i].Substring(sources[i].LastIndexOf('.')));
            }

            return dests;
        }
    }
}
