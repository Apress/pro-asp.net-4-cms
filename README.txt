CMS2010 - Build for "Pro ASP .NET 4 CMS"

GLOBALIZATION
=====================================================================
- The text of the admin site is contained in App_GlobalResources/Localization.resx.

IMPLEMENTATION NOTES
=====================================================================
- As this system is a companion to a text and intended to be a starting point for custom implementations, some design purity is 
  occasionally sacrified in favor of clarity. For instance, there are a handful of admin pages where certain behaviors are 
  handled in the code-behind as opposed to one of the libraries; in all cases this is done simply to ensure the purpose / behavior 
  is clear, and typically the majority of functionality still resides in the included libraries. I have noted this in the comments 
  of the code itself.

- Although the CMS text contains instructions on working with Memcached, there is no caching code present in this build. Caching 
  can be fairly subjective based on the intended use of the system, and I didn't want to make too many assumptions about how 
  you'd use the system (for example, the in-process cache will be faster than a distributed cache locally as well as on a 
  single-server configuration). I also didn't want readers to have to set up running console apps; with this build, you can simply
  open it in VS2010 and hit F5.

- Before launching a production instance of this CMS, consider minifying the CSS and Javascript.

- I leave the creation of user management to the reader as an exercise in creating admin pages for the CMS. The system uses
  Forms Authentication, which I would wager most .NET web developers are fairly comfortable with.

DATABASE NOTES
=====================================================================
- No indexes or other tuning steps have been applied to the included MDF files. As there are only a handful of tables required to deliver 
  core CMS functionality, I thought it best to leave as an exercise to the reader (plus I expect that some degree of customization will 
  take place as well).

- Make sure you change the connection string settings to match your system. If you move to a production environment, don't forget to
  enable connection pooling.

COMPILATION NOTES
=====================================================================
- If you receive the following error on build, clean the solution (Build => Clean Solution) to resolve the cast.
	
	The type or namespace name 'admin_masters_controls_EditEmbeddableRow' could not be found (are you missing a using directive or an assembly reference?)	
	C:\SVN\Codeplex\Web\admin\editors\editContent.aspx.cs	
	204	
	11	
	C:\...\Web\

- Don't forget that .NET doesn't allow unloading of assemblies. If you attempt to recompile the CMS while the Cassini server is running,
  you'll receive errors indicating that the post-build file copies failed. You'll need to stop the Cassini server (or stop the AppPool if
  you use IIS) to make a deployment of MEF assemblies.