USE todo;

CREATE TABLE IF NOT EXISTS Todos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    isComplete BOOLEAN NOT NULL
);
