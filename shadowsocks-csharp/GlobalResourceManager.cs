using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CCWin.SkinControl;
using System.Configuration;
using ESBasic.Loggers;
using System.IO;
using ESBasic;
using ESBasic.Helpers;

namespace Shadowsocks
{
    /// <summary>
    /// 客户端全局资源管理器。
    /// </summary>
    internal class GlobalResourceManager
    {
        #region Icon64
        private static Icon icon64;
        public static Icon Icon64
        {
            get { return icon64; }
        }
        #endregion

        #region Icon64Grey
        private static Icon icon64Grey;
        public static Icon Icon64Grey
        {
            get { return icon64Grey; }
        }
        #endregion

        #region MainBackImage
        private static Image mainBackImage;
        public static Image MainBackImage
        {
            get { return mainBackImage; }
        }
        #endregion

        #region EmotionList、EmotionDictionary
        private static List<Image> emotionList;
        public static List<Image> EmotionList
        {
            get { return emotionList; }
        }
        private static Dictionary<uint, Image> emotionDictionary;
        public static Dictionary<uint, Image> EmotionDictionary
        {
            get
            {
                if (emotionDictionary == null)
                {
                    emotionDictionary = new Dictionary<uint, Image>();
                    for (uint i = 0; i < emotionList.Count; i++)
                    {
                        emotionDictionary.Add(i, emotionList[(int)i]);
                    }
                }
                return emotionDictionary;
            }
        }
        #endregion

        #region Png64
        private static Image png64;
        public static Image Png64
        {
            get { return png64; }
        }
        #endregion






        #region Logger
        private static IAgileLogger logger = null;
        public static IAgileLogger Logger
        {
            get { return GlobalResourceManager.logger; }
        }
        #endregion

        #region UiSafeInvoker
        private static UiSafeInvoker uiSafeInvoker;
        public static UiSafeInvoker UiSafeInvoker
        {
            get { return GlobalResourceManager.uiSafeInvoker; }
        }

        public static void SetUiSafeInvoker(UiSafeInvoker invoker)
        {
            GlobalResourceManager.uiSafeInvoker = invoker;
        }
        #endregion

        #region SoftwareName
        private static string softwareName = "GGTalk";
        public static string SoftwareName
        {
            get { return GlobalResourceManager.softwareName; }
        }
        #endregion

        #region NoneIcon64
        private static Icon noneIcon64;
        public static Icon NoneIcon64
        {
            get { return noneIcon64; }
        }
        #endregion

        #region GroupIcon
        private static Icon groupIcon;
        public static Icon GroupIcon
        {
            get { return GlobalResourceManager.groupIcon; }
        }
        #endregion

        #region LoginBackImage
        private static Image loginBackImage;
        public static Image LoginBackImage
        {
            get { return loginBackImage; }
        }
        #endregion

        #region HeadImages
        private static Image[] headImages;
        public static Image[] HeadImages
        {
            get
            {
                return headImages;
            }
        }
        #endregion

        #region HeadImagesGrey
        private static Image[] headImagesGrey;
        public static Image[] HeadImagesGrey
        {
            get
            {
                return headImagesGrey;
            }
        }
        #endregion

    }
}
