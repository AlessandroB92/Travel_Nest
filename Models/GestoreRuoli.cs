using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Travel_Nest.Models
{
    public class GestoreRuoli : RoleProvider
    {
        readonly TravelDb db = new TravelDb();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                foreach (var roleName in roleNames)
                {
                    if (!IsUserInRole(username, roleName))
                    {
                        var user = db.Utenti.FirstOrDefault(u => u.Nome == username);
                        var admin = db.Admin.FirstOrDefault(a => a.Nome == username);

                        if (user != null)
                        {
                            user.Ruolo = roleName;
                            db.SaveChanges();
                        }
                        else if (admin != null)
                        {
                            admin.Ruolo = roleName;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] { };

            var userRole = db.Utenti.FirstOrDefault(u => u.Nome == username)?.Ruolo;
            if (userRole != null)
            {
                roles = new string[] { userRole };
            }

            var adminRole = db.Admin.FirstOrDefault(a => a.Nome == username)?.Ruolo;
            if (adminRole != null)
            {
                roles = new string[] { adminRole };
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRole = db.Utenti.FirstOrDefault(u => u.Nome == username)?.Ruolo;
            if (userRole != null && userRole.Equals(roleName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var adminRole = db.Admin.FirstOrDefault(a => a.Nome == username)?.Ruolo;
            if (adminRole != null && adminRole.Equals(roleName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}