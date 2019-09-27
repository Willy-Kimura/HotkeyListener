using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace WK.Libraries.HotkeyListenerNS
{
    public partial class HotkeyListener : Component
    {
        #region Constructors

        public HotkeyListener()
        {
            InitializeComponent();

            SetDefaults();
        }

        public HotkeyListener(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            SetDefaults();
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #region Browsable

        #endregion

        #region Non-browsable

        #endregion

        #endregion

        #region Methods

        #region Public

        #endregion

        #region Private

        private void SetDefaults()
        {

        }

        #endregion

        #endregion

        #region Events

        #region Public

        #endregion

        #endregion
    }
}
