using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Ano).IsRequired().HasMaxLength(4);

            builder.HasOne(x => x.Autor)
                .WithMany(x => x.Livros)
                .HasForeignKey(s => s.AutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Autor).UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
