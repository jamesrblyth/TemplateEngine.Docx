using System;
using System.Linq;

namespace TemplateEngine.Docx
{
	public delegate byte[] ImageFactory(string tag, double widthCm, double heightCm);

	[ContentItemName("Image")]
	public class ImageContent : HiddenContent<ImageContent>, IEquatable<ImageContent>
    {
        public ImageContent()
        {
            
        }

        public ImageContent(string name, byte[] binary)
        {
            Name = name;
            Binary = binary;
        }

	    public ImageContent(string name, ImageFactory factory)
	    {
		    Name = name;
		    Factory = factory;
	    }

        public byte[] Binary { get; set; }
		public ImageFactory Factory { get; set; }

        #region Equals

        public bool Equals(ImageContent other)
		{
			if (other == null) return false;

			if (!Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase)) return false;
			if (Binary == null) return Factory.Equals(other.Factory);
			return Binary.SequenceEqual(other.Binary);
		}

		public override bool Equals(IContentItem other)
		{
			if (!(other is ImageContent)) return false;

			return Equals((ImageContent)other);
		}

		public override int GetHashCode()
		{
			return new {Name, Binary, Factory}.GetHashCode();
		}

		#endregion
	}
}
