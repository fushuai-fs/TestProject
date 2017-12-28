# TestProject
  DataAccessor.MongoDBHelper
2017年11月20日 16:56:13 AutoMapper 使用测试
	2017年12月28日 14:06:04
	WebProject.Formatters BufferedMediaTypeFormatter 实现
		参照 https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/media-formatters
		http://www.intstrings.com/ramivemula/articles/simple-custom-media-formatter-in-asp-net-web-api/
	   ************
	    // Request
        // http://localhost:50276/api/Default/GetCvs
        // Accept:text/csv  
        // Response    
        // 1,BMW,Race,99999999 
		**********
		// Request
        // http://localhost:50276/api/Default/PostCvs
        // Accept:text/csv  
		// body:[{1,BMW,Race,111}{1,BMW,Race,222}{1,BMW,Race,333}]
        // Response    
            1,BMW,Race,111
			1,BMW,Race,222
			1,BMW,Race,333 