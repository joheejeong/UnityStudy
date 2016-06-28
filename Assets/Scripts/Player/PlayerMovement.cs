using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    // 플레이어가 이동하기 위한 방향 및 동작
    Vector3     movement;

    Animator    anim;
    Rigidbody   playerRigidbody;

    // floor와 레이케스트를 하기 위해
    int         floorMask;

    // 카메라에서 발사 되는 광선의 최대 길이
    float       camRayLength = 100f;

    void Awake()
    {
        // Awake () 스크립트 활성화여부에 상관없이 호출. 참조설정에 유리

        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        

    }

    void FixedUpdate()
    {
        // 물리 효과와 함께 호출

        //-1, 0, 1

        // 키보드 입력
        // -1, 0, 1값만을 가짐 그 사이값을 가지지 않아 플레이어 이동시 값의 증가로 최고속도에 도달하는 방법이 아니라 바로 최고속도에 도달하기 떄문에 반응성이 우수하다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);




    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

    }

    void Turning()
    {
        // 카메라 -> 스크린 -> 레벨(플로어 쿼트)

        // 마우스 위치의 스크린포인터를 이용해 레이를 얻는다
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        // 레이져를 쏜다
        // 스크린포인터로 지면에 히트된 포지션을 구한다
        // 맞힌경우 참을 반환, 아닐경우 false
        // floorMask는 레이가 floor에만 맞추기를 원하는지
        if(Physics.Raycast (camRay , out floorHit, camRayLength , floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            // z축이 전진축으로 기본 설정되어있어는데 여기서 playerToMouse백터를 플레이어의 전진 백터로 지정해주는 함수
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            // player가 playerToMouse백터축으로 바라볼수 있게
            playerRigidbody.MoveRotation(newRotation);


        }
    }

    void Animating(float h , float v)
    {

        // h 또는 v에 값이 존재할 경우 플레이어가 움직인 것이므로 true가 되어 
        // Animator Controll에서 Parameter로 설정한 bool형의 IsWalking에 true값을 주어 move-> idle로 상태변화가 이루어질 수 있도록
        // animator component를 통해 값을 전달해준다.

        // 그 반대로 움직임이 없이 false일 경우 IsWalking에 false를 전달하여 move->idle로 상태변화가 이루어지도록 한다.
        bool walking = h != 0f || v != 0f;

        anim.SetBool("IsWalking", walking);


    }




}
