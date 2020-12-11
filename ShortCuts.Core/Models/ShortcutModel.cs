using System.Collections;

namespace ShortCuts.Core.Models
{
    public class ShortcutModel 
    {
        #region Constructor
        public ShortcutModel() { }
        #endregion

        #region Properties
        /// <summary>
        /// Id for the Table
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product Type Id is Like Microsoft,Adobe,Corel Id etc..
        /// </summary>
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Product Type is Like Microsoft,Adobe,Corel Id etc..
        /// </summary>
        public string PropductType { get; set; }
        /// <summary>
        /// The Product Id which we are displaying Like Illustrater,Photoshop etc...
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The Product Name which we are displaying Like Illustrater,Photoshop etc...
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// The shortcut Key of specific Software Like Illustrater,Photoshop etc...  for Mac
        /// </summary>
        public string MacKey { get; set; }

        /// <summary>
        /// The shortcut Key of specific Software Like Illustrater,Photoshop etc...  for Windows
        /// </summary>
        public string WinKey { get; set; }
        /// <summary>
        /// The Description of specific Software Displaying Like Illustrater,Photoshop etc...
        /// </summary>
        public string Description { get; set;}
        /// <summary>
        /// Is this Short cut Key is Active or Not
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Is this key is Deleted or Deprecated
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion
    }
}
