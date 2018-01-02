# TestProject
  DataAccessor.MongoDBHelper
 2017年11月20日 16:56:13 AutoMapper 使用测试<br/>
 
 2017-12-27 09:09:02<br/>
 WebApiExceptionFilterAttribute 异常处理

     2017年12月28日 14:06:04   
	WebProject.Formatters BufferedMediaTypeFormatter 实现
		[参照1](https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/media-formatters)
		[参照1](http://www.intstrings.com/ramivemula/articles/simple-custom-media-formatter-in-asp-net-web-api/)
	 
	    // Request 
        // http://localhost:50276/api/Default/GetCvs 
        // Accept:text/csv  <br/>
        // Response    <br/>
        // 1,BMW,Race,99999999 
	
	
	 // Request 
        // http://localhost:50276/api/Default/PostCvs 
        // Accept:text/csv  <br/>
		// body:[{1,BMW,Race,111}{1,BMW,Race,222}{1,BMW,Race,333}] 
        // Response     
            1,BMW,Race,111 
			1,BMW,Race,222 
			1,BMW,Race,333 
			

	 // session  
            //RouteTable.Routes.MapHttpRoute(name: "DefaultApi",                
			 routeTemplate: "api/{controller}/{id}",                          
			  defaults: new { id = RouteParameter.Optional }                 
              ).RouteHandler = new SessionControllerRouteHandler();            
