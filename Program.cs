﻿using System;
using System.IO;
using System.Windows.Forms;

namespace GenericCityBuilderRPG
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
            //    using (var game = new GenericCityBuilderRPG())
            //        game.Run();
            //}
            //catch (Exception e)
            //{
            //    using (StreamWriter w = File.AppendText("error.log"))
            //    {
            //        w.WriteLine(DateTime.Now.ToString() + ": " + e.ToString());
            //    }
            //    MessageBox.Show("Unfortunately something really bad happened. Check error.log for more details.", "Error", MessageBoxButtons.OK);
            //}
            using (var game = new GenericCityBuilderRPG())
                game.Run();
        }
    }
#endif
}
