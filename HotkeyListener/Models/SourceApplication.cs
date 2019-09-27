using System;

namespace WK.Libraries.HotkeyListenerNS.Models
{
    /// <summary>
    /// Saves details of the application from
    /// where a particular Hotkey was triggered.
    /// </summary>
    public class SourceApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceApplication"/> class.
        /// </summary>
        /// <param name="id">The application's ID.</param>
        /// <param name="handle">The application's handle.</param>
        /// <param name="name">The application's name.</param>
        /// <param name="title">The application's title.</param>
        /// <param name="path">The application's path.</param>
        internal SourceApplication(int id, IntPtr handle, string name,
                                   string title, string path)
        {
            ID = id;
            Name = name;
            Path = path;
            Title = title;
            Handle = handle;
        }

        #region Properties

        /// <summary>
        /// Gets the application's process-ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the appliation's window-handle.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Gets the application's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the application's title-text.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the application's absolute path.
        /// </summary>
        public string Path { get; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="string"/> containing the list 
        /// of application details provided.
        /// </summary>
        public override string ToString()
        {
            return $"ID: {ID}; Handle: {Handle}, Name: {Name}; " +
                   $"Title: {Title}; Path: {Path}";
        }

        #endregion
    }
}