using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
	public interface ITestWriter
	{
		bool GetSomeMessage(string mess);
	}
}
