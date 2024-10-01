﻿using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class OrganizationService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public OrganizationService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<Organization>> Find()
        {
            return await _dbContext.Organizations
                .ToListAsync();
        }

        //Get (by id)
        public async Task<Organization?> Get(int id)
        {
            return await _dbContext.Organizations
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<Organization?> Create(Organization organization)
        {
            _dbContext.Organizations.Add(organization);
            await _dbContext.SaveChangesAsync();

            return organization;
        }

        //Update
        public async Task<Organization?> Update(int id, Organization organization)
        {
            var dbOrganization = _dbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (dbOrganization is null)
            {
                return null;
            }

            dbOrganization.Name = organization.Name;
            dbOrganization.Description = organization.Description;

            await _dbContext.SaveChangesAsync();

            return dbOrganization;
        }

        //Delete
        public async Task Delete(int id)
        {
            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (organization is null)
            {
                return;
            }

            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
        }

    }
}
