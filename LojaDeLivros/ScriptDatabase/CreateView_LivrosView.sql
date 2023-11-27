SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[LivrosView]
AS
SELECT        dbo.Livros.Codl, dbo.Livros.Titulo, dbo.Livros.Editora, dbo.Livros.Edicao, dbo.Livros.AnoPublicacao, dbo.Livros.Valor, dbo.Assuntos.Descricao, dbo.Autores.Nome
FROM            dbo.Livro_Autor INNER JOIN
                         dbo.Autores ON dbo.Livro_Autor.Autor_CodAu = dbo.Autores.CodAu INNER JOIN
                         dbo.Livros ON dbo.Livro_Autor.Livro_Codl = dbo.Livros.Codl INNER JOIN
                         dbo.Assuntos INNER JOIN
                         dbo.Livro_Assunto ON dbo.Assuntos.CodAs = dbo.Livro_Assunto.Assunto_CodAs ON dbo.Livros.Codl = dbo.Livro_Assunto.Livro_Codl
GO