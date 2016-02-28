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
		private string baseName = "BaseAspNetAngularUnity";

		public CodePuller()
		{
			
		}

		public string GetCode(string projectName)
		{
			if (Directory.Exists(projectName))
			{
				throw new ArgumentException("Directory : " + projectName + " exists. You must delete the directory");
			}
			Directory.CreateDirectory(projectName);

			var retval = Repository.Clone("https://github.com/wbsimms/BaseAspNetAngularUnity.git", projectName);
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
				if (file.Contains(".git")) continue;
				File.WriteAllText(file,File.ReadAllText(file).Replace(baseName,projectName));
			}
		}

	}
}
