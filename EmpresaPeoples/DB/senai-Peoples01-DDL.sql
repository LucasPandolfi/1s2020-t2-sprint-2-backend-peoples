CREATE DATABASE T_Peoples;

GO
USE T_Peoples;
GO

CREATE TABLE Funcionarios (
	IdFuncionario INT PRIMARY KEY IDENTITY,
	NomeFuncionario VARCHAR(255) NOT NULL,
	Sobrenome VARCHAR(255) NOT NULL
);