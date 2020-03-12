USE PPBeta
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Analise e desenvolvimento de sitemas', 1);
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Banco de dados', 1);
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Teste', 0);
GO

INSERT INTO Alunos(RA, Nome, CursoID, DataNascimento, Email)
VALUES(1802047, 'Erick Novais Da Hora', 1, '10/01/1994', 'erick.hora@aluno.faculdadeimpacta.com.br');
GO