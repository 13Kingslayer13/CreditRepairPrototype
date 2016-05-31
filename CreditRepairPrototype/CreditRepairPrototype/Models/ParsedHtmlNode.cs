using System;
using System.Collections.Generic;

namespace CreditRepairPrototype
{
	public class ParsedHtmlNode
	{
		string nodeClass;
		string nodeValue;
		List<ParsedHtmlNode> childNodes;

		public ParsedHtmlNode (string nodeClass, string value)
		{
			this.childNodes = new List<ParsedHtmlNode> ();
			this.nodeClass = nodeClass;
			this.nodeValue = value;
		}

		public void AddNode(ParsedHtmlNode node)
		{
			childNodes.Add (node);
		}

		public List<ParsedHtmlNode> ChildNodes {
			get {
				return childNodes;
			}
		}

		public string NodeValue {
			get {
				return nodeValue;
			}
		}

		public string NodeClass {
			get {
				return nodeClass;
			}
		}
	}
}

