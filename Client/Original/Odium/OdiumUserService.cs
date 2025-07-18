using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// Token: 0x02000008 RID: 8
public class OdiumUserService
{
	// Token: 0x0600002D RID: 45 RVA: 0x00002A5A File Offset: 0x00000C5A
	public OdiumUserService(HttpClient httpClient, string baseUrl)
	{
		this._httpClient = httpClient;
		this._baseUrl = baseUrl;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002A74 File Offset: 0x00000C74
	[DebuggerStepThrough]
	public Task<int> GetUserCountAsync()
	{
		OdiumUserService.<GetUserCountAsync>d__3 <GetUserCountAsync>d__ = new OdiumUserService.<GetUserCountAsync>d__3();
		<GetUserCountAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
		<GetUserCountAsync>d__.<>4__this = this;
		<GetUserCountAsync>d__.<>1__state = -1;
		<GetUserCountAsync>d__.<>t__builder.Start<OdiumUserService.<GetUserCountAsync>d__3>(ref <GetUserCountAsync>d__);
		return <GetUserCountAsync>d__.<>t__builder.Task;
	}

	// Token: 0x04000015 RID: 21
	private readonly HttpClient _httpClient;

	// Token: 0x04000016 RID: 22
	private readonly string _baseUrl;
}
