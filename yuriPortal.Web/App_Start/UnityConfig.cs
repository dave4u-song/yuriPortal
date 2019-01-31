using System;
using Unity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using yuriPortal.Data.Models;
using yuriPortal.Core.Interfaces;
using yuriPortal.Core.Repository;
using Unity.Injection;
using System.Web;
using yuriPortal.Web.Controllers;

namespace yuriPortal.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
			// NOTE: To load from web.config uncomment the line below.
			// Make sure to add a Unity.Configuration to the using statements.
			// container.LoadConfiguration();

			// TODO: Register your type's mappings here.
			// container.RegisterType<IProductRepository, ProductRepository>();

			container.RegisterType<ICodeGroupRepository, CodeGroupRepository>();
			container.RegisterType<ICommCodeRepository, CommCodeRepository>();
			container.RegisterType<IBoardRepository, BoardRepository>();
			container.RegisterType<IPostRepository, PostRepository>();
			container.RegisterType<ILangRepository, LangRepository>();
			container.RegisterType<IMenuRepository, MenuRepository>();
			container.RegisterType<ICustomerRepository, CustomerRepository>();


			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<RolesAdminController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());
			container.RegisterType<UsersAdminController>(new InjectionConstructor());
			container.RegisterType<UserController>(new InjectionConstructor());
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
			container.RegisterType<IUserProfileRepository, UserProfileRepository>();
		}
    }
}