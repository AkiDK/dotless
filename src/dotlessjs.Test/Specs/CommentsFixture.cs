using NUnit.Framework;

namespace dotless.Tests.Specs
{
  public class CommentsFixture : SpecFixtureBase
  {
    [Test]
    public void CommentHeader()
    {
      var input =
        @"
/******************\
*                  *
*  Comment Header  *
*                  *
\******************/
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void MultilineComment()
    {
      var input =
        @"
/*

    Comment

*/
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void MultilineComment2()
    {
      var input =
        @"
/*  
 * Comment Test
 * 
 * - dotless (http://dotlesscss.com)
 *
 */
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void LineCommentGetsRemoved()
    {
      var input = "////////////////";
      var expected = "";

      AssertLess(input, expected);
    }

    [Test]
    public void ColorInsideComments()
    {
      var input =
        @"
/* Colors
 * ------
 *   #EDF8FC (background blue)
 *   #166C89 (darkest blue)
 *
 * Text:
 *   #333 (standard text)
 *   #1F9EC9 (standard link)
 *
 */
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void CommentInsideAComment()
    {
      var input =
        @"
/*  
 * Comment Test
 * 
 *  // A comment within a comment!
 * 
 */
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void VariablesInsideComments()
    {
      var input =
        @"
/* @group Variables
------------------- */
";
      
      AssertLessUnchanged(input);
    }

    [Test]
    public void BlockCommentAfterSelector()
    {
      var input =
        @"
#comments /* boo */ {
  color: red;
}
";

      var expected = @"
#comments {
  color: red;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void EmptyComment()
    {
      var input =
        @"
#comments {
  border: solid black;
  /**/
  color: red;
}
";

      var expected =
        @"
#comments {
  border: solid black;
  /**/

  color: red;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void BlockCommentAfterProperty()
    {
      var input =
        @"
#comments {
  border: solid black;
  color: red; /* A C-style comment */
  padding: 0;
}
";

      var expected =
        @"
#comments {
  border: solid black;
  color: red;
  /* A C-style comment */

  padding: 0;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void LineCommentAfterProperty()
    {
      var input =
        @"
#comments {
  border: solid black;
  color: red; // A little comment
  padding: 0;
}
";

      var expected = @"
#comments {
  border: solid black;
  color: red;
  padding: 0;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void BlockCommentBeforeProperty()
    {
      var input =
        @"
#comments {
  border: solid black;
  /* comment */ color: red;
  padding: 0;
}
";

      var expected = @"
#comments {
  border: solid black;
  /* comment */
  color: red;
  padding: 0;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void LineCommentAfterALineComment()
    {
      var input =
        @"
#comments {
  border: solid black;
  // comment //
  color: red;
  padding: 0;
}
";

      var expected = @"
#comments {
  border: solid black;
  color: red;
  padding: 0;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void LineCommentAfterBlock()
    {
      var input =
        @"
#comments /* boo */ {
  color: red;
} // comment
";

      var expected = @"
#comments {
  color: red;
}
";

      AssertLess(input, expected);
    }

    [Test]
    public void BlockCommented()
    {
      var input =
        @"
/* commented out
  #more-comments {
    color: grey;
  }
*/
";

      AssertLessUnchanged(input);
    }

    [Test]
    public void CommentOnLastLine()
    {
      var input =
        @"
#last { color: blue }
//
";

      var expected = @"
#last {
  color: blue;
}
";

      AssertLess(input, expected);
    }

  }
}