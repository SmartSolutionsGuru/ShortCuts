namespace ShortCuts.DAL.Models
{

    /// <summary>
    /// Create Product Type Table for the First Step to Choose
    /// Product Suit Like Adobe,Microsoft etc...
    /// </summary>
    public class ProductTypeModel  : BaseModel
    {
        /// <summary>
        /// Short name of Suit
        /// </summary>
        public string ShortName { get; set; }

        private string _Description;
        /// <summary>
        /// Description For Tool Tip
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyOfPropertyChange(nameof(FullName)); }
        }


        private string _FullName;
        /// <summary>
        /// Complete Name of Suit Like
        /// Adobe Suit Microsoft Suit etc...
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; NotifyOfPropertyChange(nameof(FullName)); }
        }
    }
}
