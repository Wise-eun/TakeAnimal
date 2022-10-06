using UnityEngine;
using UnityEngine.EventSystems; // Ű����, ���콺, ��ġ�� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ����� ����

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{//�� ��ũ��Ʈ�� ��׶��忡 ����� ����
 //��ġ�� ������ �κ��� ��׶����̱� ����

    //�����̴� ������ �����ϱ� ���ؼ� ������
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    //��׶����� �������� ������ �����ų ����
    private float radius;

    //ȭ�鿡�� ������ �÷��̾�
    [SerializeField] private GameObject go_Player;
    [SerializeField] private GameObject soul_Player;
    //������ �ӵ�
    [SerializeField] private float moveSpeed;

    //��ġ�� ���۵��� �� �����̰Ŷ�
    private bool isTouch = false;
    //������ ��ǥ
    private Vector3 movePosition;


    void Start()
    {
        //inspector�� �� rect Transform�� �����ϴ� �� ����
        //0.5�� ���ؼ� �������� ���ؼ� ���� �־���
        this.radius = rect_Background.rect.width * 0.5f;
    }

    RaycastHit hit;
    //�̵� ����
    void Update()
    {
        if (this.isTouch)
        {
          //  if(Physics.Raycast(go_Player.transform.position, go_Player.transform.))
            this.go_Player.transform.position += this.movePosition;
            this.soul_Player.transform.position -= this.movePosition;
        }
    }



    //�������̽� ����

    //������ ��(��ġ�� ���۵��� ��)
    public void OnPointerDown(PointerEventData eventData)
    {
        this.isTouch = true;
    }

    //�� ���� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        //�� ���� �� ����ġ�� ������
        rect_Joystick.localPosition = Vector3.zero;

        this.isTouch = false;
        //�����̴� ���� ������ �� �ٽ� Ŭ���ϸ� ���� ������ �Ǵ� ������ ��ħ
        this.movePosition = Vector3.zero;
    }

    //�巡�� ������
    public void OnDrag(PointerEventData eventData)
    {
        //���콺 ������(x��, y�ุ �־ ����2)
        //���콺 ��ǥ���� ������ ��׶��� ��ǥ���� �� ����ŭ ���̽�ƽ(�� ���׶��)�� ������ ����
        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        //���α�
        //����2�� �ڱ��ڽ��� ����ŭ, �ִ� ��������ŭ ���Ѱ���
        value = Vector2.ClampMagnitude(value, radius);
        //(1,4)���� ������ (-3 ~ 5)���� ���α� ��

        //�θ�ü(��׶���) �������� ������ ������� ��ǥ���� �־���
        rect_Joystick.localPosition = value;

        //value�� ���Ⱚ�� ���ϱ�
        value = value.normalized;
        //x�࿡ ���⿡ �ӵ� �ð��� ���� ��
        //y�࿡ 0, ���� ���ҰŶ�
        //z�࿡ y���⿡ �ӵ� �ð��� ���� ��
        this.movePosition = new Vector3(value.y * moveSpeed * Time.deltaTime,
                                        0f,
                                        -1 * value.x * moveSpeed * Time.deltaTime);
    }
}