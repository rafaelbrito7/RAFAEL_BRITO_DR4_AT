using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(300);
            builder.Property(x => x.DtNascimento).IsRequired();

            builder.HasMany(x => x.Livros)
                .WithOne(x => x.Autor)
                .HasForeignKey(x => x.AutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Livros)
                   .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}


