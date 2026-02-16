# 📊 Customer Support Case Study – Power BI Dashboard

## 📌 Project Overview
This project analyzes customer support cases to evaluate performance, SLA compliance, and workload distribution using Power BI.

---

## 🗂 Dataset
Dataset created using SQL Server Management Studio (SSMS).

Tables:
- Cases
- SLA_Config

---

## 📈 Key KPIs
- Total Cases
- SLA Breach %
- Active Cases
- Average Resolution Time

---

## 📊 Dashboard Insights
- Cases by Status
- Cases by Priority
- Cases by Assigned Agent
- Monthly Case Trend
- Interactive Filters (Priority, Status, AssignedTo, Date)

---

## 🛠 Tools Used
- SQL Server
- Power BI Desktop
- DAX Measures
- Data Modeling

---

## 📷 Dashboard Preview
(See screenshots folder)

---

## 📌 DAX Measures Used

### Total Cases
CALCULATE(COUNT('Cases'[CaseID]))

### SLA Breach %
DIVIDE(
    CALCULATE(COUNT('Cases'[CaseID]), 'Cases'[SLAFlag] = "Yes"),
    COUNT('Cases'[CaseID])
)

---

## 🚀 Outcome
This dashboard helps management monitor SLA performance and agent workload efficiently.

