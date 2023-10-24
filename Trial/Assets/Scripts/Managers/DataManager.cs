using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using Structs;
using EnumTypes;
using System;

public class DataManager : BaseMonoBehaviour
{
    private CraftUIinfo[] craftUIinfos;
    private CraftUIinfo craftUIinfo;
    private IGameManager _gameManager;
    public Dictionary<CraftTypes, Dataset_craft> craftRequests = new Dictionary<CraftTypes, Dataset_craft>();
    private CraftTypes crafttype;
    public void Start()
    {

    }
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
            int rowCount = int.Parse(worksheet.Cells[1, 1].Text) * Paralength + 1;
            int colCount = worksheet.Dimension.Columns;
            Debug.Log(rowCount);
            Debug.Log(colCount);
            craftUIinfos = new CraftUIinfo[5];

            for (int row = 2; row <= rowCount; row += Paralength)
            {
                CraftUIinfo craftUIinfo = new CraftUIinfo(); // �� �������� ���ο� craftUIinfo ��ü ����
                craftUIinfo.name = worksheet.Cells[row, 1].Text; // ���� ù��° ���� ������ �̸� ����
                Debug.Log(craftUIinfo.name);
                craftUIinfo.imageName = worksheet.Cells[row, 2].Text; // ���� �̸� ����
                craftUIinfos[(row - 2) / Paralength] = craftUIinfo; // �����͸� ��ĵ�� �� Paralength��ŭ ���Ƿ� �׿� ���� ���� �� ������ �ϳ��� ����

                string[] itemname = new string[20]; // �� �������� ���ο� itemname �迭 ����
                int[,] itemnum = new int[3, 11]; // �� �������� ���ο� itemnum �迭 ����

                for (int j = 0; j < colCount-1; j++)
                {
                    itemname[j] = worksheet.Cells[row + 1, j + 2].Text; // �״��� �ٿ��� ������ ���� - row=1�϶��� �ʿ���� ��
                    Debug.Log(itemname[j]);
                }
                Debug.Log("clear");
                for (int i = 2; i < Paralength; i++) // i�� ���� ���� ����
                {
                    for (int j = 0; j < colCount-1; j++) // j�� ���� ������ ���� ����
                    {
                        itemnum[i - 2, j] = int.Parse(worksheet.Cells[row + i, j+2].Text); // �迭 ���� ��ġ�� �ణ �ٸ��Ƿ� ����
                        Debug.Log(itemnum[i - 2, j]);
                    }
                }

                Dataset_craft Precraft = new Dataset_craft();
                Precraft.item_name = itemname;
                Precraft.item_num = itemnum;
                craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes), craftUIinfo.name)] = Precraft;
                Debug.Log(row);
            }
            Debug.Log(craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes),"Floor")].item_num[0, 5]);
            _gameManager.GetCraftScrollviewManager().Content_Change(craftUIinfos);
        }
    }
}
