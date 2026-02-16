-- Use Database
USE FabrikamServicesDB;
GO

-- ==========================================
-- Create Cases Table
-- ==========================================
CREATE TABLE Cases (
    CaseID INT PRIMARY KEY IDENTITY(1,1),
    CreatedDate DATETIME,
    ClosedDate DATETIME NULL,
    Priority VARCHAR(20),
    Status VARCHAR(20),
    AssignedTo VARCHAR(50),
    SLA_DueDate DATETIME,
    CustomerID INT
);

-- ==========================================
-- Create SLA Configuration Table
-- ==========================================
CREATE TABLE SLA_Config (
    Priority VARCHAR(20),
    SLA_Hours INT
);

-- ==========================================
-- Insert SLA Configuration Data
-- ==========================================
INSERT INTO SLA_Config VALUES
('Low', 72),
('Medium', 48),
('High', 24);

-- ==========================================
-- Insert Cases Data
-- ==========================================
INSERT INTO Cases
(CreatedDate, ClosedDate, Priority, Status, AssignedTo, SLA_DueDate, CustomerID)
VALUES
('2026-01-01', '2026-01-02', 'High', 'Closed', 'John',  '2026-01-02', 101),
('2026-01-03', '2026-01-06', 'Medium', 'Closed', 'Sara', '2026-01-05', 102),
('2026-01-05', NULL,          'High', 'Open',   'Mike', '2026-01-06', 103),
('2026-01-07', '2026-01-09', 'Low',   'Closed', 'John', '2026-01-10', 104),
('2026-01-08', NULL,          'Medium','In Progress','Sara','2026-01-09',105),
('2026-01-10', '2026-01-12', 'High',  'Closed', 'Mike', '2026-01-11', 106),
('2026-01-12', NULL,          'Low',   'Open',  'John', '2026-01-15', 107);

-- ==========================================
-- Verify Data
-- ==========================================
SELECT * FROM Cases;
SELECT * FROM SLA_Config;

