﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dotless.CompatibilityTests
{
    public class TestPath
    {
        public const string DifferencesDir = "differences";

        private const string LessDir = @"test\less\";
        private const string CssDir = @"test\css\";

        public static IEnumerable<TestPath> LoadAll(string projectDir)
        {
            var fullLessDir = Path.Combine(projectDir, LessDir);
            var fullPaths = System.IO.Directory.EnumerateFiles(fullLessDir, "*.less", SearchOption.AllDirectories);
            var testPaths = fullPaths.Select(p => p.Replace(fullLessDir, "").Replace(".less", ""));

            return testPaths.Select(p => new TestPath(projectDir, p));
        }

        private readonly string _projectDir;
        private readonly string _testPath;

        public TestPath(string projectDir, string testPath)
        {
            _projectDir = projectDir;
            _testPath = testPath;
        }

        public string TestName
        {
            get { return _testPath; }
        }

        public string FileName
        {
            get { return Path.GetFileName(Less); }
        }

        public string Directory
        {
            get { return Path.GetDirectoryName(Less); }
        }

        public string Less
        {
            get { return Path.Combine(_projectDir, LessDir, _testPath + ".less"); }
        }

        public string Css
        {
            get { return Path.Combine(_projectDir, CssDir, _testPath + ".css"); }
        }

        public string Ignore
        {
            get { return _testPath + ".less"; }
        }

        public string ActualCss
        {
            get { return Path.Combine(DifferencesDir, _testPath + ".actual"); }
        }

        public string ExpectedCss
        {
            get { return Path.Combine(DifferencesDir, _testPath + ".expected"); }
        }
    }
}