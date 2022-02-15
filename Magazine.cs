using UnityEngine;

namespace Game.Scripts
{
    public class Magazine : MonoBehaviour
    {
        [SerializeField]
        private int m_numberOfBullet = 8;

        public int numberOfBullet
        {
            get => m_numberOfBullet;
            set => m_numberOfBullet = value;
        }
    
        public GameObject firstBullet;
        public GameObject secondBullet;

        public bool GetRemainingBullet()
        {
            if (numberOfBullet != 0)
            {
                numberOfBullet--;
                if (numberOfBullet == 0)
                {
                    firstBullet.SetActive(false);
                    secondBullet.SetActive(false);
                }
                else if (numberOfBullet == 1)
                {
                    firstBullet.SetActive(true);
                    secondBullet.SetActive(false);
                }
                else
                {
                    firstBullet.SetActive(true);
                    secondBullet.SetActive(true);
                }
                return true;
            }
            else
            {
                firstBullet.SetActive(false);
                secondBullet.SetActive(false);
                return false;
            }
        }
    }
}
