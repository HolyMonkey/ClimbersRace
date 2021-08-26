using System.Collections.Generic;
using UnityEngine;

namespace LevelView
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private List<LevelCellView> _levelCells;
        [SerializeField] private Level _level;

        private void Start()
        {
            Render(_level.CurrentLevel);
        }

        private void Render(int currentLevel)
        {
            ChangeCellStatus(currentLevel);
        }

        private void ChangeCellStatus(int currentLevel)
        {
            int minCellsLevel = 1;
            for (int i = currentLevel; i < currentLevel + _levelCells.Count; i++)
            {
                if (i % _levelCells.Count == 0)
                    minCellsLevel = i + 1 - _levelCells.Count;
            }

            Change();

            void Change()
            {
                for (int i = 0; i < _levelCells.Count; i++)
                {
                    int targetCellLevel = minCellsLevel + i;
                    _levelCells[i].SetTextValue((targetCellLevel));

                    if (targetCellLevel < currentLevel)
                        _levelCells[i].MarkPassed();
                    else if (targetCellLevel > currentLevel)
                        _levelCells[i].Downlight();
                    else if (targetCellLevel == currentLevel)
                        _levelCells[i].Highlight();
                }
            }
        }
    }
}

