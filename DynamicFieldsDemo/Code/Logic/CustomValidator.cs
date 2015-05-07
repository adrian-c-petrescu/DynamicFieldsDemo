using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Code.Logic
{
	public abstract class CustomValidator
	{
		public abstract bool IsValid(string value);
		public abstract void AddHtmlAttributes(Dictionary<string, object> attributes, string message);
	}

	public class RequiredCustomValidator : CustomValidator
	{
		public override bool IsValid(string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		public override void AddHtmlAttributes(Dictionary<string, object> attributes, string message)
		{
			attributes.Add("data-val-required", message);
		}
	}

	public class MinLengthCustomValidator : CustomValidator
	{
		private readonly int _minLength;

		public MinLengthCustomValidator(string str)
		{
			_minLength = int.Parse(str.Split(' ')[1]);	
		}

		public override bool IsValid(string value)
		{
			if (value == null)
				return true;
			return value.Length >= _minLength;
		}

		public override void AddHtmlAttributes(Dictionary<string, object> attributes, string message)
		{
			attributes.Add("data-val-minlength", message);
			attributes.Add("data-val-minlength-min", _minLength.ToString());
		}
	}

	public class MaxLengthCustomValidator : CustomValidator
	{
		private readonly int _maxLength;
		public MaxLengthCustomValidator(string str)
		{
			_maxLength = int.Parse(str.Split(' ')[1]);
		}

		public override bool IsValid(string value)
		{
			if (value == null)
				return true;
			return value.Length <= _maxLength;
		}

		public override void AddHtmlAttributes(Dictionary<string, object> attributes, string message)
		{
			attributes.Add("data-val-maxlength", message);
			attributes.Add("data-val-maxlength-max", _maxLength.ToString());
		}
	}

	public class RegexCustomValidator : CustomValidator
	{
		private readonly string _regexPattern;

		public RegexCustomValidator(string str)
		{
			var firstSpaceIdx = str.IndexOf(' ');
			_regexPattern = str.Substring(firstSpaceIdx + 1);
		}

		public override bool IsValid(string value)
		{
			return Regex.IsMatch(value, _regexPattern);
		}

		public override void AddHtmlAttributes(Dictionary<string, object> attributes, string message)
		{
			attributes.Add("data-val-regex", message);
			attributes.Add("data-val-regex-pattern", _regexPattern);
		}
	}

}