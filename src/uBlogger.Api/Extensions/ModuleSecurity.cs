//using Nancy;
//
//public static class ModuleSecurity
//{
//    public static void RequiresAuthentication(this NancyModule module)
//    {
//        module.Before.AddItemToEndOfPipeline(RequiresAuthentication);
//    }
//
//    private static Response RequiresAuthentication(NancyContext context)
//    {
//        Response response = null;
//        if (context.CurrentUser == null)
//        {
//            response = new Response { StatusCode = HttpStatusCode.Unauthorized };
//        }
//
//        return response;
//    }
//
//}