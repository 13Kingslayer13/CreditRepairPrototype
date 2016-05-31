using System;

namespace CreditRepairPrototype
{
	public class ParseHtmlSettings
	{
		string attribute;
		string attributeValue;
		string inclusion;
		bool mustHaveInclusion;
		bool hasAttribute;

		public ParseHtmlSettings (string attribute, string attributeValue)
		{
			this.attribute = attribute;
			this.attributeValue = attributeValue;
			hasAttribute = true;
			mustHaveInclusion = false;
		}

		public ParseHtmlSettings (string attribute)
		{
			this.attribute = attribute;
			this.attributeValue = null;
			hasAttribute = false;
			mustHaveInclusion = false;
		}

		public string Attribute {
			get {
				return attribute;
			}
		}

		public string AttributeValue {
			get {
				return attributeValue;
			}
		}

		public string ContentInclusion {
			get {
				return inclusion;
			}
			set {
				inclusion = value;
				mustHaveInclusion = true;
			}
		}

		public bool HasAttributeValue
		{
			get
			{
				return hasAttribute;
			}
		}

		public bool HasInclusion
		{
			get
			{
				return mustHaveInclusion;
			}
		}
	}
}

