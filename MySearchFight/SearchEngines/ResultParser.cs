﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MySearchFight.SearchEngines
{
    [XmlInclude(typeof(DefaultResultParser))]
    public abstract class ResultParser
    {
        public static readonly ResultParser Default = new DefaultResultParser();

        public abstract long Parse(string result);
    }

    public class DefaultResultParser : ResultParser
    {
        public override long Parse(string result)
        {
            if(result == null)
                throw new ArgumentNullException("result");

            try
            {
                return long.Parse(result.Replace(",", "").Replace(".", ""));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
          
        }
    }
}
