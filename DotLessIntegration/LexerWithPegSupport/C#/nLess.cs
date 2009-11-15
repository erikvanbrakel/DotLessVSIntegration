/* created on 12/11/2009 17:30:19 from peg generator V1.0 using '' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace nLess
{
      
      enum EnLess{Parse= 1, Syntax= 2, LeftCurly= 3, RightCurly= 4, LeftSquare= 5, 
                   RightSquare= 6, Comma= 7, SemiColon= 8, Colon= 9, Identifier= 10, 
                   Variable= 11, Class= 12, Id= 13, Ident= 14, CommentPart= 15, 
                   DoubleSlashComment= 16, SlashStarComment= 17, SlashStarCommentStart= 18, 
                   SlashStarCommentEnd= 19, WS= 20, ws= 21, s= 22, S= 23, ns= 24};
      class nLess : PegCharParser 
      {
        
         #region Input Properties
        public static EncodingClass encodingClass = EncodingClass.ascii;
        public static UnicodeDetection unicodeDetection = UnicodeDetection.notApplicable;
        #endregion Input Properties
        #region Constructors
        public nLess()
            : base()
        {
            
        }
        public nLess(string src,TextWriter FerrOut)
			: base(src,FerrOut)
        {
            
        }
        #endregion Constructors
        #region Overrides
        public override string GetRuleNameFromId(int id)
        {
            try
            {
                   EnLess ruleEnum = (EnLess)id;
                    string s= ruleEnum.ToString();
                    int val;
                    if( int.TryParse(s,out val) ){
                        return base.GetRuleNameFromId(id);
                    }else{
                        return s;
                    }
            }
            catch (Exception)
            {
                return base.GetRuleNameFromId(id);
            }
        }
        public override void GetProperties(out EncodingClass encoding, out UnicodeDetection detection)
        {
            encoding = encodingClass;
            detection = unicodeDetection;
        } 
        #endregion Overrides
		#region Grammar Rules
        public bool Parse()    /*^^Parse:  (Syntax / Identifier / CommentPart) * ;*/
        {

           return TreeNT((int)EnLess.Parse,()=>
                OptRepeat(()=>  
                      Syntax() || Identifier() || CommentPart() ) );
		}
        public bool Syntax()    /*Syntax: ws (LeftCurly / RightCurly / LeftSquare / RightSquare / Comma / SemiColon / Colon) ws;*/
        {

           return And(()=>  
                     ws()
                  && (    
                         LeftCurly()
                      || RightCurly()
                      || LeftSquare()
                      || RightSquare()
                      || Comma()
                      || SemiColon()
                      || Colon())
                  && ws() );
		}
        public bool LeftCurly()    /*^^LeftCurly : '{';*/
        {

           return TreeNT((int)EnLess.LeftCurly,()=> Char('{') );
		}
        public bool RightCurly()    /*^^RightCurly : '}';*/
        {

           return TreeNT((int)EnLess.RightCurly,()=> Char('}') );
		}
        public bool LeftSquare()    /*^^LeftSquare : '[';*/
        {

           return TreeNT((int)EnLess.LeftSquare,()=> Char('[') );
		}
        public bool RightSquare()    /*^^RightSquare : ']';*/
        {

           return TreeNT((int)EnLess.RightSquare,()=> Char(']') );
		}
        public bool Comma()    /*^^Comma : ',';*/
        {

           return TreeNT((int)EnLess.Comma,()=> Char(',') );
		}
        public bool SemiColon()    /*^^SemiColon : ';';*/
        {

           return TreeNT((int)EnLess.SemiColon,()=> Char(';') );
		}
        public bool Colon()    /*^^Colon : ':';*/
        {

           return TreeNT((int)EnLess.Colon,()=> Char(':') );
		}
        public bool Identifier()    /*Identifier: ws (Variable / Class / Id / Ident) ws;*/
        {

           return And(()=>  
                     ws()
                  && (    Variable() || Class() || Id() || Ident())
                  && ws() );
		}
        public bool Variable()    /*^^Variable: '@' [-_a-zA-Z0-9]+;*/
        {

           return TreeNT((int)EnLess.Variable,()=>
                And(()=>  
                     Char('@')
                  && PlusRepeat(()=>    
                      (In('a','z', 'A','Z', '0','9')||OneOf("-_")) ) ) );
		}
        public bool Class()    /*^^Class:  '.' [_a-zA-Z] [-a-zA-Z0-9_]*;*/
        {

           return TreeNT((int)EnLess.Class,()=>
                And(()=>  
                     Char('.')
                  && (In('a','z', 'A','Z')||OneOf("_"))
                  && OptRepeat(()=>    
                      (In('a','z', 'A','Z', '0','9')||OneOf("-_")) ) ) );
		}
        public bool Id()    /*^^Id: '#' [_a-zA-Z] [-a-zA-Z0-9_]*;*/
        {

           return TreeNT((int)EnLess.Id,()=>
                And(()=>  
                     Char('#')
                  && (In('a','z', 'A','Z')||OneOf("_"))
                  && OptRepeat(()=>    
                      (In('a','z', 'A','Z', '0','9')||OneOf("-_")) ) ) );
		}
        public bool Ident()    /*^^Ident: [-_a-zA-Z0-9]+;*/
        {

           return TreeNT((int)EnLess.Ident,()=>
                PlusRepeat(()=>  
                  (In('a','z', 'A','Z', '0','9')||OneOf("-_")) ) );
		}
        public bool CommentPart()    /*CommentPart : DoubleSlashComment / SlashStarComment;*/
        {

           return     DoubleSlashComment() || SlashStarComment();
		}
        public bool DoubleSlashComment()    /*^^DoubleSlashComment : '/' '/'  (!([\n]/[\r\n]/[\r]).)*;*/
        {

           return TreeNT((int)EnLess.DoubleSlashComment,()=>
                And(()=>  
                     Char('/')
                  && Char('/')
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> OneOf("\n\r\n\r") ) && Any() ) ) ) );
		}
        public bool SlashStarComment()    /*SlashStarComment: SlashStarCommentStart / SlashStarCommentEnd;*/
        {

           return     SlashStarCommentStart() || SlashStarCommentEnd();
		}
        public bool SlashStarCommentStart()    /*^^SlashStarCommentStart : '/' '*' (!(SlashStarCommentEnd).)*;*/
        {

           return TreeNT((int)EnLess.SlashStarCommentStart,()=>
                And(()=>  
                     Char('/')
                  && Char('*')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=> SlashStarCommentEnd() )
                            && Any() ) ) ) );
		}
        public bool SlashStarCommentEnd()    /*^^SlashStarCommentEnd : '*' '/';
//******************************************** Common*/
        {

           return TreeNT((int)EnLess.SlashStarCommentEnd,()=>
                And(()=>    Char('*') && Char('/') ) );
		}
        public bool WS()    /*WS: [ \r\n\t]+;*/
        {

           return PlusRepeat(()=> OneOf(" \r\n\t") );
		}
        public bool ws()    /*ws: [ \r\n\t]*;*/
        {

           return OptRepeat(()=> OneOf(" \r\n\t") );
		}
        public bool s()    /*s:  [ ]*;*/
        {

           return OptRepeat(()=> OneOf(" ") );
		}
        public bool S()    /*S:  [ ]+;*/
        {

           return PlusRepeat(()=> OneOf(" ") );
		}
        public bool ns()    /*ns: ![ ;\n] .;*/
        {

           return And(()=>    Not(()=> OneOf(" ;\n") ) && Any() );
		}
		#endregion Grammar Rules
   }
}