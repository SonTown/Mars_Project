using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using Structs;

public class DataManager : BaseMonoBehaviour
{
    private CraftUIinfo[] craftUIinfos;
    private CraftUIinfo craftUIinfo;
    private IGameManager _gameManager;
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }

    public void Init(IGameManager gameManager)
    {
        Debug.Log("DataManager Ready");
        _gameManager = gameManager;
    }
    public void Excelread(string Filename)
    {
        // Excel ���� ���
        string excelFilePath = Filename;

        // Excel ���� �ε�
        FileInfo fileInfo = new FileInfo(excelFilePath);
        using (var package = new ExcelPackage(fileInfo))
        {
            // ���ϴ� ��Ʈ ���� (��Ʈ �̸�����)
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Overall"];
            worksheet = package.Workbook.Worksheets[worksheet.Cells[1, 1].Text];
            // ������ �б� ����]
            int Paralength = int.Parse(worksheet.Cells[1, 2].Text);
            int rowCount = int.Parse(worksheet.Cells[1,1].Text)*Paralength+1;
            int colCount = worksheet.Dimension.Columns;
            Debug.Log(rowCount);
            Debug.Log(colCount);
            craftUIinfos = new CraftUIinfo[5];
            for (int row = 2; row <= rowCount; row+=Paralength)
            {
                craftUIinfo.name= worksheet.Cells[row, 1].Text;
                craftUIinfo.imageName = worksheet.Cells[row, 2].Text;
                craftUIinfos[(row-2)/Paralength] = craftUIinfo;
            }
            _gameManager.GetCraftScrollviewManager().Content_Change(craftUIinfos);
        }
    }
}
