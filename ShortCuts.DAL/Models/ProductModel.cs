namespace ShortCuts.DAL.Models
{
    /// <summary>
    /// Class that will display and hold 
    /// the Name and Other Information Of Products
    /// </summary>
    public class ProductModel :BaseModel
    {
        #region Constructor
        public ProductModel() { }
        #endregion

        #region Properties
        /// <summary>
        /// Product Type Id is Like Microsoft,Adobe,Corel Id etc..
        /// </summary>
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Product Short Name Like Illistratar,Photoshop etc...
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// The Product Name which we are 
        /// Displaying Like Illustrater,Photoshop etc...
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Verify is this Key Depricated or Not
        /// </summary>
        public bool? IsDepricated { get; set; }
        /// <summary>
        /// The Description of specific Software 
        /// Displaying Like Illustrater,Photoshop etc...
        /// </summary>
        public string Description { get; set;}

        #endregion
    }
}
