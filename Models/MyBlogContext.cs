﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS58_Razor09EF.Models
{
	public class MyBlogContext : IdentityDbContext<AppUser>
	{
		public DbSet<Article> Articles { get; set; }
	
		public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
		{
			//...
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()) { 
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet")) {
					entityType.SetTableName(tableName.Substring(6));

                }
			}
		}
	}
}
