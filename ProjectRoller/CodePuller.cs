﻿#region license
// Copyright (c) 2016, Wm. Barrett Simms
// 
// Permission to use, copy, modify, and/or distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
// 
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace ProjectRoller
{
	public class CodePuller
	{
		private string baseName, baseNameV1 = "BaseAspNetAngularUnity";
		private string baseNameV2 = "CoreAspNetAngular";

		public CodePuller()
		{
			
		}

		public string GetCode(string version, string projectName)
		{
			if (Directory.Exists(projectName))
			{
				throw new ArgumentException("Directory : " + projectName + " exists. You must delete the directory");
			}
			Directory.CreateDirectory(projectName);

			var repo = "https://github.com/wbsimms/BaseAspNetAngularUnity.git";
			baseName = baseNameV1;
			if (version == "v2")
			{
				repo = "https://github.com/wbsimms/CoreAspNetAngular.git";
				baseName = baseNameV2;
			}

			var retval = Repository.Clone(repo, projectName);
			try
			{
				Directory.Delete(projectName + "/.git", true);
			}
			catch {}
			RenameDirectories(projectName);
			RenameFiles(projectName);
			RewriteFiles(projectName);
			return retval;
		}

		private void RenameDirectories(string projectName)
		{
			var directories = Directory.GetDirectories(projectName,"*",SearchOption.AllDirectories);
			foreach (var directory in directories)
			{
				if (directory.Contains(".git")) continue;
				if (directory.Contains(baseName))
				{
					string newName = directory.Replace(baseName, projectName);
					if (!Directory.Exists(newName))
						Directory.Move(directory,newName);
				}
			}
		}

		public void RenameFiles(string projectName)
		{
			string[] files = Directory.GetFiles(projectName, "*.*", SearchOption.AllDirectories);
			foreach (string file in files)
			{
				if (file.Contains(".git")) continue;
				if (file.Contains(baseName))
				{
					string newFileName = file.Replace(baseName, projectName);
					File.Move(file,newFileName);
				}
			}
		}

		public void RewriteFiles(string projectName)
		{
			string[] files = Directory.GetFiles(projectName, "*.*", SearchOption.AllDirectories);
			foreach (string file in files)
			{
				if (file.Contains(".git") || Path.GetExtension(file) == ".exe") continue;
				File.WriteAllText(file,File.ReadAllText(file).Replace(baseName,projectName));
			}
		}

	}
}
